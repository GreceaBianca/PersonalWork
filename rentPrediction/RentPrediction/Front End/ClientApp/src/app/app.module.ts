import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

// App Core Components
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { LayoutComponent } from './components/layout/layout.component';


import { HttpRequestStatusInterceptor } from './interceptors/http-request-status.interceptor';

import { LayoutResolver } from './resolvers/layout.resolver';

import { AccessGuard } from './guards/access.guard';

import { AppRoutingModule } from './app-routing.module';
import { SharedModule } from './shared/shared.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';
import { HeaderComponent } from './components/layout/header/header.component';
import { LoaderComponent } from './components/layout/loader/loader.component';
import { SidenavComponent } from './components/layout/sidenav/sidenav.component';
import { ContactComponent } from './components/contact/contact.component';
import { PropertyCardComponent } from './components/properties/property-card/property-card.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { PropertiesComponent } from './components/properties/properties/properties.component';
import { PropertyComponent } from './components/properties/properties/property/property.component';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { LogoutComponent } from './components/logout/logout.component';
import { UserManagementComponent } from './components/user-management/user-management.component';
import { EditUserComponent } from './components/user-management/edit-user/edit-user.component';
import { ChangePasswordComponent } from './components/user-management/change-password/change-password.component';
import { UserPropertiesComponent } from './components/user-management/user-properties/user-properties.component';
import { UserFavoritePropertiesComponent } from './components/user-management/user-favorite-properties/user-favorite-properties.component';
import { AdminManagementComponent } from './components/user-management/admin-management/admin-management.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { QuestionsComponent } from './components/questions/questions.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LayoutComponent,
    UnauthorizedComponent,
    HeaderComponent,
    LoaderComponent,
    SidenavComponent,
    PropertiesComponent,
    ContactComponent,
    PropertyCardComponent,
    LoginComponent,
    RegisterComponent,
    PropertyComponent,
    LogoutComponent,
    UserManagementComponent,
    EditUserComponent,
    ChangePasswordComponent,
    UserPropertiesComponent,
    UserFavoritePropertiesComponent,
    AdminManagementComponent,
    QuestionsComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),
    BrowserAnimationsModule,
    PaginationModule.forRoot(),
    CarouselModule.forRoot()
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpRequestStatusInterceptor,
      multi: true
    },
    AccessGuard,
    LayoutResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
