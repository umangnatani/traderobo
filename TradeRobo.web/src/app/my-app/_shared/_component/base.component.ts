
import { ApiService, NotificationService, Globals } from 'app/my-app/_shared';



export abstract class BaseComponent {

    Globals = {IsBusy: false};

    constructor(protected apiService: ApiService) {
        this.apiService.resetPageGlobals();
    }
}
