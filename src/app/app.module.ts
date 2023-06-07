import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { ComunityPageComponent } from './comunity-page/comunity-page.component';
import { FeedPageComponent } from './feed-page/feed-page.component';
import { HomePageComponent } from './home-page/home-page.component';
import { LoginPageComponent } from './login-page/login-page.component';
import { NewAccountPageComponent } from './new-account-page/new-account-page.component';
import { NotFoundPageComponent } from './not-found-page/not-found-page.component';
import { RecoverPageComponent } from './recover-page/recover-page.component';
import { UserPageComponent } from './user-page/user-page.component';
import { PasswordComponent } from './password/password.component';
import { CpfComponent } from './cpf/cpf.component';
import { VerifyCpfComponent } from './verify-cpf/verify-cpf.component'; // Adicionado para poder usar o ngModel
import { CreatePasswordComponent } from './create-password/create-password.component'; // Added for use ngModel
import { HttpClientModule } from '@angular/common/http'; // Added for use HttpClient
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // Added for use ReactiveForms
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatButtonModule } from '@angular/material/button'; // Added for use Angular Material Button
import {Component, Inject} from '@angular/core';
import {MatDialog, MAT_DIALOG_DATA, MatDialogRef, MatDialogModule} from '@angular/material/dialog';
import {NgIf} from '@angular/common';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {FormBuilder, Validators, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatStepperModule} from '@angular/material/stepper';

@NgModule({
  declarations: [
    CreatePasswordComponent,
    NewAccountPageComponent,
    ComunityPageComponent,
    NotFoundPageComponent,
    RecoverPageComponent,
    LoginPageComponent,
    VerifyCpfComponent,
    UserPageComponent,
    PasswordComponent,
    FeedPageComponent,
    HomePageComponent,
    AppComponent,
    NavComponent,
    CpfComponent,
  ],
  imports: [
    BrowserAnimationsModule, // Added for use ReactiveForms
    MatSlideToggleModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    HttpClientModule, // Added for use HttpClient
    AppRoutingModule,
    MatStepperModule,
    MatButtonModule, // Added for use Angular Material Button
    MatDialogModule,
    MatInputModule,
    BrowserModule,
    FormsModule, // Adicionado para poder usar o ngModel
    FormsModule,
    NgIf,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
