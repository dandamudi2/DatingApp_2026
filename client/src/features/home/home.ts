import { Component, Input, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { RegisterComponent } from "../account/register";
import { User } from '../../types/user';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule, RegisterComponent],
  templateUrl: './home.html',
  styleUrls: ['./home.css']
})
export class HomeComponent {

  // controls whether the register view is shown
  protected registerMode = signal(false);




  ShowRegisterBtn(value:boolean){

    this.registerMode.set(value);
  }
}
