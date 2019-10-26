import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
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
@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        CounterComponent,
        FetchDataComponent,
        LoginComponent,
        RegisterComponent,
        ControllerListComponent,
        ControllerItemComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        BrowserModule,
        ReactiveFormModule,
        MatProgressBarModule,
        MatFormFieldModule,
        MatInputModule,
        FontAwesomeModule,
        RouterModule.forRoot([
            { path: '', component: ControllerListComponent },
            { path: 'auth', component: LoginComponent },
            { path: "register", component: RegisterComponent }
        ])
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTOR,
            useClass: AuthInterceptor,
            multi: true
        },
        AuthService,
        RegisterService,
        ControllerService
    ],
    bootstrap: [AppComponent],
    exports: [
        MatProgressBarModule,
        MatFormFieldModule,
        MatInputModule
    ]
})
export class AppModule { }
