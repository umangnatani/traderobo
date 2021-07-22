import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FuseSharedModule } from '@fuse/shared.module';
import { MatCardModule } from '@angular/material/card';

import { ContentComponent } from '.';

@NgModule({
    declarations: [
        ContentComponent
    ],
    imports     : [
        RouterModule,
        FuseSharedModule,
        MatCardModule
    ],
    exports     : [
        ContentComponent
    ]
})
export class ContentModule
{
}
