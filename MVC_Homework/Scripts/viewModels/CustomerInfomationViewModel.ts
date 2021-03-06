﻿

class CustomerInfomationViewModel {
    /**
     * API URL
     */
    apiUrl: string;

    /**
     * 資料
     */
    customers: KnockoutObservableArray<CustomerInfomation> = ko.observableArray([]);

    /**
     * 當前頁數
     */
    currentPage = ko.observable(1);

    /**
     * 總共頁碼
     */
    pages = ko.observableArray([1]);

    /*
     * 總頁數
     */
    pageCount = ko.observable(1);

    /*
     * 關鍵字
     */
    keyword = ko.observable("");

    /*
     * 排序欄位
     */
    sortField = ko.observable("Id");

    /**
     * 排序方式
     */
    sortOrder = ko.observable("ASC");

    constructor(apiUrl: string) {
        this.apiUrl = apiUrl;

        var queryOption: QueryOption = {
            keyword: "",
            page: 1,
            sortField: "Id",
            sortOrder: "ASC"
        };

        console.log(`constructor`);
        this.updateDatas(queryOption);
    }

    pageClick = (page: number) => {
        console.log(`pageClick Input : ${page}`);
        if (page != undefined && this.currentPage() !== page && page > 0 && page <= this.pageCount()) {

            this.currentPage(page);

            var queryOption = this.getCurrentQueryOption();

            this.updateDatas(queryOption);
        }
    };

    searchClick = (form: HTMLFormElement) => {
        console.log(`submit keyword : ${this.keyword()}`);
        
        //if(!$(form).valid())
        //    return false;

        var queryOption = this.getCurrentQueryOption();
        queryOption.page = 1;

        this.updateDatas(queryOption);
    };

    sortClick = (field: string) => {
        if (field === undefined)
            return;

        if (this.sortField() === field) {
            if (this.sortOrder() === "ASC")
                this.sortOrder("DESC");
            else
                this.sortOrder("ASC");
        } else {
            this.sortField(field);
            this.sortOrder("ASC");
        }

        var queryOption = this.getCurrentQueryOption();

        this.updateDatas(queryOption);
    };

    /**
     * 呼叫 API
     */
    updateDatas = (queryOption: QueryOption) => {

        console.log("post data:");
        console.log(queryOption);

        $.ajax({
            url: this.apiUrl,
            method: "POST",
            data: queryOption,
            error: (error) => {
                console.log(error);
                alert("失敗");
            },
            success: (datas: QueryOptionResult<CustomerInfomation[]>) => {
                this.customers.removeAll();
                this.customers.push(...datas.datas);
                this.pageCount(datas.pageCount);
                this.currentPage(datas.page);
                this.keyword(datas.keyword);

                this.pages.removeAll();
                for (let i = 1; i <= datas.pageCount; i++) {
                    this.pages.push(i);
                }

                console.log(datas);
            }
        });
    };

    /*
     * 取得當前設定
     */
    getCurrentQueryOption = () => {
        var queryOption: QueryOption = {
            keyword: this.keyword(),
            page: this.currentPage(),
            sortField: this.sortField(),
            sortOrder: this.sortOrder()
        };

        return queryOption;
    };

    buildSortClass = (field: string) =>
        ko.pureComputed(() => {
            var sortIcon = "glyphicon glyphicon-sort";

            if (field === this.sortField()) {
                sortIcon += "-by-alphabet";

                if (this.sortOrder() === "DESC") {
                    sortIcon += "-alt";
                }
            }

            return sortIcon;

        });
}

interface CustomerInfomation {
    客戶名稱: string,
    銀行數量: number,
    聯絡人數量: number,
}

interface QueryOption {
    keyword: string,
    page: number,
    sortOrder: string,
    sortField: string,
}

interface QueryOptionResult<T> extends  QueryOption {
    pageCount: number,
    datas: T,
}