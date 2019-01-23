import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../user.service';
import { Credentials } from '../../credentials';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css'],
  providers: [UserService]
})
export class LoginFormComponent implements OnInit {
  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;
  credentials: Credentials = { email: '', password: '' };

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit() {
    // Przejdz do glownej strony jesli juz jest zalogowany
    if (this.userService.isLoggedIn()) {
      this.router.navigate(['']);
    }
  }

  login({ value, valid }: { value: Credentials, valid: boolean }) {
    this.submitted = true;
    this.isRequesting = true;
    this.errors = '';
    if (valid) {
      this.userService.login(value.email, value.password)
        .then(
          result => {
            if (result) {
              this.router.navigate(['']);
              alert("Zalogowano pomyślnie.");
            }
          },
        error => {
          this.errors = error;
          this.isRequesting = false;
        }
      );
    }
  }

}
