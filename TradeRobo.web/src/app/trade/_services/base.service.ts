import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { environment } from 'environments/environment';

@Injectable({
    providedIn: 'root'
})
export class BaseService {


    token: ApiModel.JwtToken;

    httpOptions;
    protected baseURL = environment.apiUrl;

    PageGlobals = {Title: '', Message: '', isError: false, isLoading: false};


    constructor(
        public httpClient: HttpClient,
        public toastrService: ToastrService
    ) {
        this.token = JSON.parse(localStorage.getItem('token'));
    }


    public setTile(val: string){
        this.PageGlobals.Title = val;
    }

    public setError(msg){
        this.PageGlobals.isError = true;
        this.PageGlobals.isLoading = false;
        this.PageGlobals.Message = msg;
    }

    public setMessage(msg){
        this.PageGlobals.isError = false;
        this.PageGlobals.isLoading = false;
        this.PageGlobals.Message = msg;
    }



    public resetPageGlobals(){
        this.PageGlobals.isError = false;
        this.PageGlobals.isLoading = false;
        this.PageGlobals.Message = '';
    }


    public setHeaders() {


        const accessToken = this.token?.accessToken;

        // console.log(accessToken);

        this.httpOptions = {
            headers: new HttpHeaders({
                AccessToken: accessToken
            })
        };
        // }

    }

    public showSuccess(msg: string) {
        this.toastrService.success(msg);
    }
    

    public get(url: string) {
        this.setHeaders();
        const uri = this.baseURL + url;
        return this.httpClient.get(uri, this.httpOptions);
    }

    public post(url: string, data, setHeader = true) {
        if (setHeader) {
            this.setHeaders();
        }
        const uri = this.baseURL + url;
        return this.httpClient.post(uri, data, this.httpOptions);
    }







}
