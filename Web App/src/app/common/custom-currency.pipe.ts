import { Pipe, PipeTransform } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { HttpService } from '../services';

@Pipe({
  name: 'customCurrency'
})
export class CustomCurrencyPipe implements PipeTransform {
  currency: string;
  symbol: string = "";
  constructor(private currencyPipe: CurrencyPipe,
    private httpService: HttpService, ) {
    this.getCulture();
  }
  getCulture() {
    this.httpService.getConfig().subscribe((appSettings) => {
      this.currency = appSettings.culture.currency;
    })
  }

  transform(value: any): string {
    if (value != null)
      return this.currencyPipe.transform(value, this.currency, this.currency);

    return this.currencyPipe.transform(0, this.currency, this.currency).split('0.00')[0];
  }
}