import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../user.service';
import { UserRegistration } from '../../user.registration';


@Component({
  selector: 'app-registration-form',
  templateUrl: './registration-form.component.html',
  styleUrls: ['./registration-form.component.css'],
  providers: [UserService]
})
export class RegistrationFormComponent implements OnInit {
  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    // Przejdz do glownej strony jesli juz jest zarejsetrowany
    if (this.userService.isLoggedIn()) {
      alert("Wyloguj się, aby dokonać rejestracji.");
      this.router.navigate(['']);
    }
  }


  registerUser({ value, valid }: { value: UserRegistration, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';

    if (valid) {
      this.userService
        .register(value.email, value.password, value.confirmPassword, value.firstName, value.lastName)
        .subscribe(
          result => {
            if (result) {
              alert("Zarejestrowano pomyślnie.");
              this.router.navigate(['/login']);
            }
          },
          errors => {
            this.errors = errors;
            this.isRequesting = false;
          });
    }
      
  }
}
