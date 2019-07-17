import { Component } from "@angular/core"
import { Router } from "@angular/router"

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ["./login.component.css"]
})

export class LoginComponent {
  public email: string;
  public password: string;

  public form = new FormData();

  constructor(private router: Router) {

  }

  signIn() {
    sessionStorage.setItem("authorize-user", "true");
    this.router.navigate(['/']);
  }
}
