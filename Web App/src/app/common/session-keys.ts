import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class SessionKeys {
    Keys = {
        BookSeller: {
            Details: "BookSeller.Details",
            // ViewCart:{
            //     BookingCart:"Shipment.ViewCart.BookingCart",
            //     SelectedBookingCartItemId:"Shipment.ViewCart.SelectedBookingCartItemId",
            //     ShipmentType:"Shipment.ViewCart.ShipmentType",
            //     SelectedShipmentMode:"Shipment.ViewCart.SelectedShipmentMode"
            // },

        },
        School: {
            Details: "School.Details",
        },
        Class: {
            Details: "Class.Details",
        },
        Publisher: {
            Details: "Publisher.Details",
        },
        Book: {
            Details: "Book.Details",
        }
    }
}