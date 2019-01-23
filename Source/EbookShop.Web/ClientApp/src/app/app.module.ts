import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NavMenu1Component } from './nav-menu1/nav-menu1.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { StopkaComponent } from './stopka/stopka.component';
import { DisplayEbook } from './display-ebook/display-ebook.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { RegistrationFormComponent } from './account/registration-form/registration-form.component';
import { LoginFormComponent } from './account/login-form/login-form.component';
import { SpinnerComponent } from './spinner/spinner.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    NavMenu1Component,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    StopkaComponent,
    DisplayEbook,
    RegistrationFormComponent,
    LoginFormComponent,
    SpinnerComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    HttpModule,
    NgxPaginationModule,
    RouterModule.forRoot([
      { path: '', component: DisplayEbook, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'register', component: RegistrationFormComponent },
      { path: 'login', component: LoginFormComponent },
    ])
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
