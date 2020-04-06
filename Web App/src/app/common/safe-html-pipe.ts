import { DomSanitizer } from '@angular/platform-browser'
import { Pipe, PipeTransform, SecurityContext } from '@angular/core';

@Pipe({ name: 'safeHtml' })
export class SafeHtmlPipe implements PipeTransform {
    constructor(private dom: DomSanitizer) { }
    transform(html) {
        let securedHtml = this.dom.bypassSecurityTrustHtml(html);
        return securedHtml;
    }
}