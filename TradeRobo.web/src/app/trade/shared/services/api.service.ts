import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService extends BaseService {



  constructor(httpClient: HttpClient, toastrService: ToastrService) { super(httpClient, toastrService); }

 

  public getAccount() {
    return this.get('RH/account');
  }

  public getFolio() {
    return this.get('RH/folio');
  }

  
  public test() {
    return this.get('RH/test');
  }

  public placeOrder(data) {
    return this.post('RH/order', data);
  }

  public placeFolioOrder(data) {
    return this.post('RH/order/folio', data);
  }


  public login(data) {
    return this.post('RH/login', data, false);
  }


}
