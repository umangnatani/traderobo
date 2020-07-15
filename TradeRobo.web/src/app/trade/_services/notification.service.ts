import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

    private symbolSource = new BehaviorSubject('KBE');
    currentSymbol = this.symbolSource.asObservable();

    private titleSource = new BehaviorSubject(null);
    currentTitle = this.titleSource.asObservable();

    private errorSource = new BehaviorSubject('');
    currentError = this.errorSource.asObservable();

  constructor() { }

  changSymbol(symbol: string) {
    this.symbolSource.next(symbol);
  }

  setTitle(val: string) {
    this.titleSource.next(val);
  }

  setError(val: string) {
    this.errorSource.next(val);
  }

}
