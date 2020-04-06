import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
export class BroadcasterModel {
    key: string
    data: any;
}
@Injectable({
    providedIn: 'root'
})
export class BroadCasterService {
    private subject = new Subject<BroadcasterModel>();
    broadcast(key, data) {
        this.subject.next({ key: key, data: data });
    }
    on(key): Observable<any> {
        return this.subject.pipe(filter(m => m.key == key), map(m => m.data));
    }
}
