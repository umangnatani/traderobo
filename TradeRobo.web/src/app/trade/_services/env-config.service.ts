import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { HttpClient } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { map, shareReplay } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class EnvConfigService  {

    private configuration$: Observable<ApiModel.Configuration>;

    BaseUrl: string;

    constructor(private httpClient: HttpClient) { 

      }

      
      public load() {
        this.getConfig().toPromise().then(data => {
            // console.log(data);
            this.BaseUrl = data.apiUrl;
            console.log(this.BaseUrl);
            return data;
        });
    }
    

    public getConfig(): Observable<ApiModel.Configuration> {
        if (!this.configuration$) {
          this.configuration$ = this.httpClient
            .get<ApiModel.Configuration>('assets/config.json')
            .pipe(shareReplay(1));
        }
        return this.configuration$;
      }
  
}
