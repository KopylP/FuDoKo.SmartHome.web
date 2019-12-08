import { BrowserModule, HAMMER_GESTURE_CONFIG } from '@angular/platform-browser';
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
import { EditControllerComponent } from './components/edit-controller/edit-controller.component';
import { EditControllerService } from './services/edit-controller.service';
import { AddButtonComponent } from './components/add-button/add-button.component';
import { MatDialogModule, MatOptionModule, MatSelectModule, MatRadioButton, MatRadioModule, MatChipsModule, MatIconModule, MatNativeDateModule, GestureConfig } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatMenuModule } from '@angular/material/menu';
import { SensorService } from './services/sensor.service';
import { SensorListComponent } from './components/sensor-list/sensor-list.component';
import { SensorItemComponent } from './components/sensor-item/sensor-item.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { SensorEditService } from './services/sensor-edit.service';
import { SensorTypeService } from './services/sensor-type.service';
import { SensorEditComponent } from './components/sensor-edit/sensor-edit.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { SensorHubService } from './services/sensor-hub.service';
import { DeviceListComponent } from './components/device-list/device-list.component';
import { DeviceItemComponent } from './components/device-item/device-item.component';
import { EditDeviceComponent } from './components/edit-device/edit-device.component';
import { ControllerAccessComponent } from './components/controller-access/controller-access.component';
import { ScriptListComponent } from './components/script-list/script-list.component';
import { ScriptItemComponent } from './components/script-item/script-item.component';
import { ScriptEditComponent } from './components/script-edit/script-edit.component';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDatepickerModule, MatDatepicker } from '@angular/material/datepicker';
import { ScriptEditService } from './services/script-edit.service';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatSliderModule } from '@angular/material/slider';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        LoginComponent,
        RegisterComponent,
        ControllerListComponent,
        ControllerItemComponent,
        HomeComponent,
        EditControllerComponent,
        AddButtonComponent,
        SensorListComponent,
        SensorItemComponent,
        SensorEditComponent,
        DeviceListComponent,
        DeviceItemComponent,
        EditDeviceComponent,
        ControllerAccessComponent,
        ScriptListComponent,
        ScriptItemComponent,
        ScriptEditComponent
    ],
    entryComponents: [
        EditControllerComponent,
        SensorEditComponent,
        EditDeviceComponent,
        ControllerAccessComponent,
        ScriptEditComponent
    ],
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        BrowserModule,
        ReactiveFormsModule,
        MatProgressBarModule,
        MatFormFieldModule,
        MatDialogModule,
        BrowserAnimationsModule,
        MatInputModule,
        MatMenuModule,
        MDBBootstrapModule.forRoot(),
        FontAwesomeModule,
        MatSlideToggleModule,
        MatOptionModule,
        MatSnackBarModule,
        MatRadioModule,
        MatChipsModule,
        MatIconModule,
        MatCheckboxModule,
        MatSelectModule,
        MatDatepickerModule,
        MatButtonToggleModule,
        MatNativeDateModule,
        MatSliderModule,
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
        AuthTrueGuard,
        EditControllerService,
        SensorService,
        SensorEditService,
        SensorTypeService,
        SensorHubService,
        { provide: HAMMER_GESTURE_CONFIG, useClass: GestureConfig }
    ],
    bootstrap: [AppComponent],
    exports: [
        MatProgressBarModule,
        MatFormFieldModule,
        MatInputModule
    ]
})
export class AppModule { }
