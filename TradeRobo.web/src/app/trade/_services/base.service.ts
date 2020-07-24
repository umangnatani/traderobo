import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { EnvConfigService } from '.';

@Injectable({
    providedIn: 'root'
})
export class BaseService {


    isRHAuthenticated: boolean;

    protected baseURL = environment.apiUrl;
    // protected baseURL: string;

    PageGlobals = { Title: '', Message: '', isError: false, isLoading: false };



    constructor(
        public toastrService: ToastrService, public configService: EnvConfigService
    ) {
        this.isRHAuthenticated = JSON.parse(localStorage.getItem('isRHAuthenticated'));
    }


    public setRHLoggedIn(): void {

        this.isRHAuthenticated = true;
    }

    public getUrl(url: string) {
        return this.baseURL + url;
    }



    public setTile(val: string) {
        this.PageGlobals.Title = val;
    }

    public setError(msg) {
        this.PageGlobals.isError = true;
        this.PageGlobals.isLoading = false;
        this.PageGlobals.Message = msg;
    }

    public setMessage(msg) {
        this.PageGlobals.isError = false;
        this.PageGlobals.isLoading = false;
        this.PageGlobals.Message = msg;
    }

    // public setMessage2(returnType: ApiModel.ReturnType) {
    //     this.PageGlobals.isError = !returnType.Success;
    //     this.PageGlobals.isLoading = false;
    //     this.PageGlobals.Message = returnType.Message;
    // }

    public setMessage2(returnType: ApiModel.ReturnType) {
        if (returnType.Success) {
            this.toastrService.success(returnType.Message);
        }
        else {
            this.toastrService.error(returnType.Message);
        }
        this.PageGlobals.isLoading = false;
        // this.PageGlobals.Message = returnType.Message;
    }



    public resetPageGlobals() {
        this.PageGlobals.isError = false;
        this.PageGlobals.isLoading = false;
        this.PageGlobals.Message = '';
    }


    public showSuccess(msg: string) {
        this.toastrService.success(msg);
    }









}
