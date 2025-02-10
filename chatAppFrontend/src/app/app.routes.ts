import { Routes } from '@angular/router';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { LogInComponent } from './components/log-in/log-in.component';
import { AuthGuard } from './guard/auth-guard.guard';

export const routes: Routes = [
    {
        path: '',
        component: LogInComponent
    },
    {
        path: 'login',
        component: LogInComponent
    },
    {
        path: 'sign-up',
        component: SignUpComponent
    },
    {
        path : 'dashboard',
        component: DashboardComponent,
        canActivate: [AuthGuard]        
    }
];
