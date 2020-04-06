import { Injectable } from '@angular/core';
import 'rxjs';
import { throwError as _throw } from 'rxjs';
import { Broadcaster } from '../helpers/broadcaster';


@Injectable({ providedIn: 'root' })
export class SessionService {

  constructor(private broadcaster: Broadcaster) {
    this.broadcaster.on('NewAccessToken').subscribe((token: string) => {
      let currentUser = this.getLoggedInUser();
      if (currentUser && token) {
        currentUser.AccessToken = token;
        this.setSession('currentUser', currentUser);
      }
    });
  }

  setSession(key: string, value: any): void {
    localStorage.setItem(key, JSON.stringify(value));
  }

  getSession(key: string): any {
    if (typeof window !== 'undefined') {
      let retrievedObject = localStorage.getItem(key) as string;
      return retrievedObject;
    }
  }
  getSessionAsJSON(key: string): any {
    if (typeof window !== 'undefined') {
      let retrievedObject = JSON.parse(localStorage.getItem(key) as string);
      return retrievedObject;
    }
  }
  deleteSession(key): void {
    localStorage.removeItem(key);
  }
  clearSession(): void {
    localStorage.clear();
  }

  getLoggedInUser(): any {
    let user = this.getSession("currentUser");
    return user ? JSON.parse(user) : null;
  }

  
}