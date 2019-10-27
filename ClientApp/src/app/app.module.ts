import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { AuthService } from './services/auth.service';
import { LoginComponent } from './components/login/login.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { RegisterService } from './services/register.service';
import { RegisterComponent } from './components/register/register.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { ControllerListComponent } from './components/controller-list/controller-list.component';
import { ControllerItemComponent } from './components/controller-item/controller-item.component';
import { ControllerService } from './services/controller.service';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { AuthTrueGuard } from './guards/auth.true.guard';
@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,
        ControllerListComponent,
        ControllerItemComponent,
        HomeComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        BrowserModule,
        ReactiveFormsModule,
        MatProgressBarModule,
        MatFormFieldModule,
        MatInputModule,
        MDBBootstrapModule.forRoot(),
        FontAwesomeModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, canActivate: [AuthGuard] },
            { path: 'auth', component: LoginComponent, canActivate: [AuthTrueGuard] },
            { path: "register", component: RegisterComponent, canActivate: [AuthTrueGuard] }
        ])
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
        },
        AuthService,
        RegisterService,
        ControllerService,
        AuthGuard,
        AuthTrueGuard
    ],
    bootstrap: [AppComponent],
    exports: [
        MatProgressBarModule,
        MatFormFieldModule,
        MatInputModule
    ]
})
export class AppModule { }
