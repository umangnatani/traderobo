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



    public getAuthUrl() {
        return this.httpClient.get(this.getUrl('/TD/authurl'));
    }

    public getAccount() {
        return this.httpClient.get(this.getUrl('/RH/account'));
    }

    public getPies(): Observable<ApiModel.Pie[]> {
        return this.httpClient.get<ApiModel.Pie[]>(this.getUrl('/pie'));
    }

    public getTDAccounts(): Observable<ApiModel.TDAccount[]> {
        return this.httpClient.get<ApiModel.TDAccount[]>(this.getUrl('/TD/accounts'));
    }

    public getWatchlists(): Observable<ApiModel.Watchlist[]> {
        return this.httpClient.get<ApiModel.Watchlist[]>(this.getUrl('/pie/watchlist'));
    }


    public test() {
        return this.httpClient.get(this.getUrl('/RH/test'));
    }


    public placeComplexOrder(data, type: string, broker: string, accountId:string): Observable<ApiModel.ReturnType> {
        if (type === 'single') {
            return this.placeOrder(data, broker, accountId);
        }
        else {
            return this.placeBulkOrder(data, broker, accountId);
        }
    }

    public placeOrder(data, broker: string, accountId:string): Observable<ApiModel.ReturnType> {
        if (broker === 'RH') {
            return this.placeRHOrder(data);
        }
        else {
            return this.placeTDOrder(data, accountId);
        }
    }

    public placeBulkOrder(data, broker: string, accountId: string): Observable<ApiModel.ReturnType> {
        if (broker === 'RH') {
            return this.placeBulkRHOrder(data);
        }
        else {
            return this.placeBulkTDOrder(data, accountId);
        }
    }


    public getPositions() {
        return this.httpClient.get(this.getUrl('/RH/position'));
    }

    public getWatchlistSymbols(watchlistId) {
        return this.httpClient.get(this.getUrl(`/pie/multi/${watchlistId}`));
    }

    public getBuyBackPositions() {
        return this.httpClient.get(this.getUrl('/RH/buyback'));
    }

    public buyBack(data) {
        return this.httpClient.post(this.getUrl('/RH/buyback'), data);
    }

    public placeBulkRHOrder(data): Observable<ApiModel.ReturnType> {
        return this.httpClient.post<ApiModel.ReturnType>(this.getUrl('/RH/order/multi'), data);
    }

    public placeBulkTDOrder(data, accountId): Observable<ApiModel.ReturnType> {
        return this.httpClient.post<ApiModel.ReturnType>(this.getUrl(`/TD/order/multi/${accountId}`), data);
    }


    public getMenu(): Observable<FuseNavigation[]> {
        return this.httpClient.get<FuseNavigation[]>(this.getUrl('/user/menu'));
    }

    public placeRHOrder(data): Observable<ApiModel.ReturnType> {
        return this.httpClient.post<ApiModel.ReturnType>(this.getUrl('/RH/order'), data);
    }

    public placeTDOrder(data, accountId: string): Observable<ApiModel.ReturnType> {
        return this.httpClient.post<ApiModel.ReturnType>(this.getUrl(`/TD/order/${accountId}`), data);
    }

    public placeFolioOrder(data): Observable<ApiModel.ReturnType> {
        return this.httpClient.post<ApiModel.ReturnType>(this.getUrl('/RH/order/pie'), data);
    }

    public login(data): Observable<ApiModel.RHAuthResponse> {
        return this.httpClient.post<ApiModel.RHAuthResponse>(this.getUrl('/RH/login'), data);
    }

    public TDlogin(data): Observable<ApiModel.TDAuthResponse> {
        return this.httpClient.post<ApiModel.TDAuthResponse>(this.getUrl('/TD/login'), data);
    }

    public saveUser(data) {
        return this.httpClient.post(this.getUrl('/user'), data);
    }

    public deleteSymbol(id, data) {
        return this.httpClient.post(this.getUrl(`/pie/delete/${id}`), data);
    }

    public changePassword(data): Observable<ApiModel.ReturnType> {
        return this.post('/user/changepassword', data);
    }

    public saveProfile(data): Observable<ApiModel.ReturnType> {
        return this.post('/user/profile', data);
    }

    public post(url, data): Observable<ApiModel.ReturnType> {
        return this.httpClient.post<ApiModel.ReturnType>(this.getUrl(url), data);
    } 


    public savePie(data): Observable<ApiModel.ReturnType> {
        return this.httpClient.post<ApiModel.ReturnType>(this.getUrl('/pie'), data);
    }

    public saveWatchListSymbols(id, data): Observable<ApiModel.ReturnType> {
        return this.httpClient.post<ApiModel.ReturnType>(this.getUrl(`/pie/watchlist/${id}`), data);
    }

    public savePieDetail(data) {
        return this.httpClient.post(this.getUrl('/Pie/save'), data);
    }

    public getPieDetail(PieId, flag: string = ''): Observable<ApiModel.PieDetail[]> {
        return this.httpClient.get<ApiModel.PieDetail[]>(this.getUrl(`/Pie/detail/${PieId}/${flag}`));
    }

    // public getFavStocks(): Observable<ApiModel.FavStocks[]> {
    //     return this.httpClient.get<ApiModel.FavStocks[]>(this.getUrl('/Pie/fav'));
    // }







}
