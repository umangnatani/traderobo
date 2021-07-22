import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { FuseSidebarModule } from '@fuse/components';
import { FuseSharedModule } from '@fuse/shared.module';

import { ChatPanelModule } from 'app/layout/components/chat-panel/chat-panel.module';

import { FooterModule } from 'app/layout/components/footer/footer.module';

import { ContentModule } from 'app/layout/components/content/content.module';
import { QuickPanelModule } from './components/quick-panel/quick-panel.module';
import { ToolbarModule } from './components/toolbar/toolbar.module';
import { NavbarModule } from './components/navbar/navbar.module';
import { VerticalLayout1Component } from './layout-1/layout-1.component';

@NgModule({
    declarations: [
        VerticalLayout1Component
    ],
    imports     : [
        RouterModule,

        FuseSharedModule,
        FuseSidebarModule,

        ChatPanelModule,
        ContentModule,
        FooterModule,
        NavbarModule,
        QuickPanelModule,
        ToolbarModule
    ],
    exports     : [
        VerticalLayout1Component
    ]
})
export class LayoutModule
{
}
