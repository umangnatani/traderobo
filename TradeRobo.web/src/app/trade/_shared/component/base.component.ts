
import { ApiService } from 'app/trade/_services';



export abstract class BaseComponent {

    Globals = {IsBusy: false};

    constructor(protected apiService: ApiService) {
        this.apiService.resetPageGlobals();
    }
}
