import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { BaseService } from './base.service';
import { map, shareReplay } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { EnvConfigService } from '.';
import { FuseNavigation } from '@fuse/types';


@Injectable({
    providedIn: 'root'
})
export class ApiService extends BaseService {


    constructor(private httpClient: HttpClient, toastrService: ToastrService, configService: EnvConfigService) {
        super(toastrService, configService);
    }



    public getAccount() {
        return this.httpClient.get(this.getUrl('/RH/account'));
    }

    public getPies(): Observable<ApiModel.Pie[]> {
        return this.httpClient.get<ApiModel.Pie[]>(this.getUrl('/pie'));
    }


    public test() {
        return this.httpClient.get(this.getUrl('/RH/test'));
    }

    public placeOrder(data, type: string): Observable<ApiModel.ReturnType> {
        if (type === 'RH') {
            return this.placeRHOrder(data);
        }
        else {
            return this.placeTDOrder(data);
        }
    }


    public getMenu(): Observable<FuseNavigation[]> {
        return this.httpClient.get<FuseNavigation[]>(this.getUrl('/user/menu'));
    }

    public placeRHOrder(data): Observable<ApiModel.ReturnType> {
        return this.httpClient.post<ApiModel.ReturnType>(this.getUrl('/RH/order'), data);
    }

    public placeTDOrder(data): Observable<ApiModel.ReturnType> {
        return this.httpClient.post<ApiModel.ReturnType>(this.getUrl('/TD/order'), data);
    }

    public placeFolioOrder(data): Observable<ApiModel.ReturnType> {
        return this.httpClient.post<ApiModel.ReturnType>(this.getUrl('/RH/order/pie'), data);
    }

    public login(data): Observable<ApiModel.RHAuthResponse> {
        return this.httpClient.post<ApiModel.RHAuthResponse>(this.getUrl('/RH/login'), data);
    }

    public saveUser(data) {
        return this.httpClient.post(this.getUrl('/user'), data);
    }


    public savePie(data): Observable<ApiModel.ReturnType> {
        return this.httpClient.post<ApiModel.ReturnType>(this.getUrl('/pie'), data);
    }

    public savePieDetail(data) {
        return this.httpClient.post(this.getUrl('/Pie/save'), data);
    }

    public getPieDetail(PieId, flag: string = ''): Observable<ApiModel.PieDetail[]> {
        return this.httpClient.get<ApiModel.PieDetail[]>(this.getUrl(`/Pie/detail/${PieId}/${flag}`));
    }

    public getFavStocks(): Observable<ApiModel.FavStocks[]> {
        return this.httpClient.get<ApiModel.FavStocks[]>(this.getUrl('/Pie/fav'));
    }







}
