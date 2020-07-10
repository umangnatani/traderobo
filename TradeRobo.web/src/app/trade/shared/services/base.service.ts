import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class BaseService {


    token: ApiModel.JwtToken;

    httpOptions;
    //baseURL = 'https://traderobo20200702134616.azurewebsites.net/api/';
    //baseURL = 'http://localhost/traderobo/api/';
    baseURL = 'https://localhost:5001/api/';
    




    constructor(
        public httpClient: HttpClient,
        public toastrService: ToastrService
    ) {
        this.token = JSON.parse(localStorage.getItem('token'));
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
