import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from './common/material-module';
import { NumberDirective,TwoDigitDecimalDirective } from './directives';
import { UppercaseDirective } from './directives/uppercase.directive';
import { ModalModule } from 'ngx-bootstrap';

// // import { BrowserModule } from '@angular/platform-browser';
// // AoT requires an exported function for factories
// export function HttpLoaderFactory(httpClient: HttpClient) {
//   return new TranslateHttpLoader(httpClient);
// }


@NgModule({
  declarations: [
    NumberDirective,
    UppercaseDirective,
    TwoDigitDecimalDirective,
  ],
  imports:[
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ModalModule.forRoot()
    // MaterialModule,
    // TranslateModule.forChild({
    //   loader: {
    //     provide: TranslateLoader,
    //     useFactory: HttpLoaderFactory,
    //     deps: [HttpClient]
    //   }
    // }),
  ],
  providers: [
    DatePipe,
  ],
  entryComponents:[],
  exports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    NumberDirective,
    UppercaseDirective,
    TwoDigitDecimalDirective
  ],
})
export class SharedModule { }
