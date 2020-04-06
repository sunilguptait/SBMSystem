import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class APIUrls {
    Urls = {
        Login: "Account/Login",
        GetNewToken: "Account/GetNewToken",
        BookSeller: {
            Create: "BookSeller/Create",
            List: "BookSeller/List",
            GetBookSellerDropdown: "BookSeller/GetBookSellerDropdown",
            CreateBookSellerSchoolMapping: "BookSeller/CreateBookSellerSchoolMapping",
            GetBookSellerSchoolMappingList: "BookSeller/GetBookSellerSchoolMappingList",
            DeleteBookSellerSchool: "BookSeller/DeleteBookSellerSchool"
        },
        Common: {
            GetStates: "Common/GetStates",
            GetCities: "Common/GetCities",
            GetBookTypes: "Common/GetBookTypes",
        },
        School: {
            Create: "School/Create",
            List: "School/List",
            GetDropdown: "School/GetSchoolDropdown",
            Delete: "School/Delete",
        },
        Class: {
            Create: "Class/Create",
            List: "Class/List",
            GetClassDropdown: "Class/GetClassDropdown",
        },
        Publisher: {
            Create: "Publisher/Create",
            List: "Publisher/List",
            ListForDropDown: "Publisher/GetListForDropdown",
        },
        Book: {
            Create: "Book/Create",
            List: "Book/List",
            GetBookDropdown: "Book/GetBookDropdown",
            CreateBookClassMapping: "Book/CreateBookClassMapping",
            GetBookClassMappingList: "Book/GetBookClassMappingList",
            DeleteBookClassMapping: "Book/DeleteBookClassMapping",
            GetClassBooksForStudent: "Book/GetClassBooksForStudent"
        },
        Order: {
            SearchStudent: 'Student/List',
            CreateOrder: 'Order/Create',
            List: 'Order/List',
            GetOrder: 'Order/GetOrder',
            Invoice: 'PDF/Invoice',
            SaveStudent: 'Student/BookSellerSaveStudent',
            ImportStudents: 'Student/ImportStudents'
        }
    }
}