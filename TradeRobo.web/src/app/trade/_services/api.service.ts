import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { BaseService } from './base.service';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ApiService extends BaseService {


  constructor(httpClient: HttpClient, toastrService: ToastrService) { 
      super(httpClient, toastrService); 
    }


  public getAccount() {
    return this.get('/RH/account');
  }

  public getFolio() {
    return this.get('/Pie');
  }

  
  public test() {
    return this.get('/RH/test');
  }

  public placeOrder(data) {
    return this.post('/RH/order', data);
  }

  public placeTDOrder(data) {
    return this.post('/TD/order', data);
  }

  public placeFolioOrder(data) {
    return this.post('/RH/order/pie', data);
  }

  public login(data) {
    return this.post('/RH/login', data, false);
  }

  public savePieDetail(data){
    return this.httpClient.post(`${this.baseURL}/Pie/save`, data);
  }

  public getPieDetail(PieId): Observable<ApiModel.PieDetail[]>{
    return this.httpClient.get<ApiModel.PieDetail[]>(`${this.baseURL}/Pie/detail/${PieId}`);
  }

  public getFavStocks(): Observable<ApiModel.FavStocks[]> {
    return this.httpClient.get<ApiModel.FavStocks[]>(`${this.baseURL}/Pie/fav`);
  }


  

 


}
