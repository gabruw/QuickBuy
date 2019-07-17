import { Injectable } from "@angular/core";
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class RoutGuard implements CanActivate {
  constructor(private router: Router) {

  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | import("@angular/router").UrlTree | import("rxjs").Observable<boolean | import("@angular/router").UrlTree> | Promise<boolean | import("@angular/router").UrlTree> {
    var authorize = sessionStorage.getItem("authorize-user");
    if (authorize == "true") {
      return true;
    } else {
      this.router.navigate(['/login'], { queryParams: {returnUrl: state.url} });

      return false;
    }
  }
}
