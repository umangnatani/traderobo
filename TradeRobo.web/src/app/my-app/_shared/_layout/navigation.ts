import { FuseNavigation } from '@fuse/types';

export const navigation: FuseNavigation[] = [
    {
        id       : 'robinhood',
        title    : 'Robinhood',
        translate: 'Robinhood',
        type     : 'group',
        icon     : 'apps',
        children : [
            {
                id       : 'Trade',
                title    : 'Trade',
                translate: 'Trade',
                type     : 'item',
                icon     : 'attach_money',
                url      : '/trade/robinhood'
            },
           
        ]
    },
    {
        id      : 'Account',
        title   : 'Account',
        type    : 'group',
        icon    : 'pages',
        children: [
            {
                id       : 'Manage Pie',
                title    : 'ManagePie',
                translate: 'Manage Pie',
                type     : 'item',
                icon     : 'pie_chart',
                url      : '/trade/pie'
            },
            {
                id       : 'Preferences',
                title    : 'Preferences',
                translate: 'Preferences',
                type     : 'item',
                icon     : 'room_preferences',
                url      : '/trade/pref'
            },
            
           
        ]
    },
    {
        id      : 'TD Ameritrade',
        title   : 'TD Ameritrade',
        type    : 'group',
        icon    : 'pages',
        children: [
            {
                id       : 'Trade',
                title    : 'Trade',
                translate: 'Trade',
                type     : 'item',
                icon     : 'attach_money',
                url      : '/trade/td'
            },
           
        ]
    },
    {
        id      : 'Admin',
        title   : 'Admin',
        type    : 'group',
        icon    : 'pages',
        children: [
            {
                id       : 'Manage Settings',
                title    : 'ManageSettings',
                translate: 'Manage Settings',
                type     : 'item',
                icon     : 'settings',
                url      : '/trade/settings'
            },
            {
                id       : 'Manage User',
                title    : 'ManageUser',
                translate: 'Manage User',
                type     : 'item',
                icon     : 'account_circle',
                url      : '/trade/user'
            },
            
           
        ]
    },
];
