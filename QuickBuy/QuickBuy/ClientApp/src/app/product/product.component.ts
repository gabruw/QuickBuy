import { Component } from "@angular/core"

@Component({
  selector: "app-product",
  templateUrl: "./product.component.html",
})

// export == public
export class ProductComponent {
  private name: string;
  private releaseToSale: boolean;

  // Get's
  public getName(): string {
    return this.name;
  }

  public setName(value: string): void {
    this.name = value;
  }

  // Set's
  public getReleaseToSale(): boolean {
    return this.releaseToSale;
  }

  public setReleaseToSale(value: boolean): void {
    this.releaseToSale = value;
  }
}
