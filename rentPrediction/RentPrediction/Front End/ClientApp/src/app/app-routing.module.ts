import { NgModule } from '@angular/core';
import { Routes, RouterModule, Route } from '@angular/router';
import { LayoutComponent } from './components/layout/layout.component';
import { LayoutResolver } from './resolvers/layout.resolver';
import { HomeComponent } from './components/home/home.component';
import { UserRoles } from './models/users/UserRole';
import { AccessGuard } from './guards/access.guard';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';

import { ContactComponent } from './components/contact/contact.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { PropertiesComponent } from './components/properties/properties/properties.component';
import { PropertyComponent } from './components/properties/properties/property/property.component';
import { UserManagementComponent } from './components/user-management/user-management.component';
import { EditUserComponent } from './components/user-management/edit-user/edit-user.component';
import { UserPropertiesComponent } from './components/user-management/user-properties/user-properties.component';
import { UserFavoritePropertiesComponent } from './components/user-management/user-favorite-properties/user-favorite-properties.component';
import { ChangePasswordComponent } from './components/user-management/change-password/change-password.component';
import { AdminManagementComponent } from './components/user-management/admin-management/admin-management.component';
import { QuestionsComponent } from './components/questions/questions.component';


export const baseAppRoutes: { [key: string]: Route } = {
  defaultRoute: { path: '', redirectTo: 'home', pathMatch: 'full' },
  homeRoute: { path: 'home', component: HomeComponent },
  propertiesRoute: { path: 'properties', component: PropertiesComponent, canActivate: [AccessGuard], data: { minUserRole: UserRoles.Users }  },
  propertyRoute: { path: 'property/:id', component: PropertyComponent, canActivate: [AccessGuard], data: { minUserRole: UserRoles.Users } },
  contactRoute: { path: 'contact', component: ContactComponent },
  loginRoute: { path: 'login', component: LoginComponent },
  registerRoute: { path: 'register', component: RegisterComponent },
  questionRoute: { path: 'questions', component: QuestionsComponent },
  userRoute: { path: 'account', component: UserManagementComponent, canActivate: [AccessGuard], data: { minUserRole: UserRoles.Users }, children:[
    {path: '', redirectTo:'edit', pathMatch: 'full'},
    {path: 'edit', component:EditUserComponent, canActivate: [AccessGuard], data: { minUserRole: UserRoles.Users }},
    {path: 'my-properties', component:UserPropertiesComponent, canActivate: [AccessGuard], data: { minUserRole: UserRoles.Users }},
    {path: 'favorites', component:UserFavoritePropertiesComponent, canActivate: [AccessGuard], data: { minUserRole: UserRoles.Users }},
    {path: 'change-password', component:ChangePasswordComponent, canActivate: [AccessGuard], data: { minUserRole: UserRoles.Users }},
    {path: 'admin', component:AdminManagementComponent, canActivate: [AccessGuard], data: { minUserRole: UserRoles.Admin }},
  ] },
  unauthorizedRoute: { path: 'unauthorized', component: UnauthorizedComponent }
}

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    resolve: {
      user: LayoutResolver
    },
    children: [
      baseAppRoutes.defaultRoute,
      baseAppRoutes.homeRoute,
      baseAppRoutes.registerRoute,
      baseAppRoutes.questionRoute,
      baseAppRoutes.propertiesRoute,
      baseAppRoutes.propertyRoute,
      baseAppRoutes.loginRoute,
      baseAppRoutes.userRoute,
      baseAppRoutes.contactRoute,
    ]
  },
  baseAppRoutes.unauthorizedRoute,
];


@NgModule({
  imports: [RouterModule.forRoot(routes, { enableTracing: false })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
