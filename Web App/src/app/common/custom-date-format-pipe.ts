import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from "@angular/common";

@Pipe({
    name: 'customDateFormat'
})


export class CustomDateFormatPipe extends DatePipe implements PipeTransform {

    transform(value: any, format: string = 'dd/MM/YYYY'): any {
        return super.transform(value, format);
    }
}