import { Component, inject, input, output, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, NgForm } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { RegisterCred, User } from '../../types/user';
import { AccountService } from '../../core/Services/account.service';


@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './register.html',
  styleUrls: ['./register.css']
})
export class RegisterComponent {

    // decare the creds variable of type User
  protected creds = {} as RegisterCred;

  protected accountService = inject(AccountService);

 

  //declare output property to pass the cancel (boolean) to parent component
  CancelRegister = output<boolean>();

  //call the account service register method from register.ts, define error 
  register() {
    
    this.accountService.register(this.creds).subscribe({
      next: (res) => {
        console.log('Register response:', res);
        this.creds={} as RegisterCred;
         this.Cancel();
      },
      error: (err) => console.error('Register error:', err),
    });
  }

  Cancel(){

    this.CancelRegister.emit(false);
  }
}
