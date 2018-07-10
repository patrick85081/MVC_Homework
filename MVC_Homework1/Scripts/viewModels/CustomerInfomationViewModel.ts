﻿

class CustomerInfomationViewModel {

    apiUrl: string;
    customers: KnockoutObservableArray<CustomerInfomation> = ko.observableArray([]);
    currentPage = ko.observable(1);
    pages = ko.observableArray([]);
    pageCount = ko.observable(1);
    keyword = ko.observable("");

    sortField = ko.observable("客戶名稱");
    sortOrder = ko.observable("");

    constructor(apiUrl: string) {
        this.apiUrl = apiUrl;

        var queryOption: QueryOption = {
            Keyword: "",
            Page: 1,
            SortField: "客戶名稱",
            SortOrder: "ASC"
        };

        $.ajax({
            url: this.apiUrl, 
            method: "POST",
            data: queryOption,
            error: (error) => {
                
            },
            success: (datas: QueryOptionResult<CustomerInfomation[]>) => {
                this.customers.removeAll();
                this.customers.push(...datas.Datas);
                this.pageCount(datas.PageCount);

                var _pages = Array(datas.PageCount).map((value: number, index: number) => index + 1);
                this.pages.removeAll();
                this.pages.push(..._pages);
                
                console.log(datas);

            }
        });
    }
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