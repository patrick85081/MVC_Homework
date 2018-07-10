

class CustomerInfomationViewModel {

    apiUrl: string;
    customers: KnockoutObservableArray<CustomerInfomation> = ko.observableArray([]);
    currentPage = ko.observable(1);
    pages = ko.observableArray([1]);
    pageCount = ko.observable(1);
    keyword = ko.observable("");

    sortField = ko.observable("Id");
    sortOrder = ko.observable("ASC");

    constructor(apiUrl: string) {
        this.apiUrl = apiUrl;

        var queryOption: QueryOption = {
            Keyword: "",
            Page: 1,
            SortField: "Id",
            SortOrder: "ASC"
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
        console.log(this.keyword());
        
        //if(!$(form).valid())
        //    return false;

        var queryOption = this.getCurrentQueryOption();
        queryOption.Page = 1;

        this.updateDatas(queryOption);
    };

    updateDatas = (queryOption: QueryOption) => {

        console.log("post data");
        console.log(queryOption);

        $.ajax({
            url: this.apiUrl,
            method: "POST",
            data: queryOption,
            error: (error) => {
                console.log(error);
            },
            success: (datas: QueryOptionResult<CustomerInfomation[]>) => {
                this.customers.removeAll();
                this.customers.push(...datas.Datas);
                this.pageCount(datas.PageCount);

                this.pages.removeAll();
                for (let i = 1; i <= datas.PageCount; i++) {
                    this.pages.push(i);
                }

                console.log(datas);
            }
        });
    };

    getCurrentQueryOption = () => {
        var queryOption: QueryOption = {
            Keyword: this.keyword(),
            Page: this.currentPage(),
            SortField: this.sortField(),
            SortOrder: this.sortOrder()
        };

        return queryOption;
    };
}

interface CustomerInfomation {
    客戶名稱: string,
    銀行數量: number,
    聯絡人數量: number,
}

interface QueryOption {
    Keyword: string,
    Page: number,
    SortOrder: string,
    SortField: string,
}

interface QueryOptionResult<T> extends  QueryOption {
    PageCount: number,
    Datas: T,
}