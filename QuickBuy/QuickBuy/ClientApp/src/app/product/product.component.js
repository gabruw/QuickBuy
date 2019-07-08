"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// export == public
var ProdutoComponent = /** @class */ (function () {
    function ProdutoComponent() {
    }
    // Get's
    ProdutoComponent.prototype.getName = function () {
        return this.name;
    };
    ProdutoComponent.prototype.setName = function (value) {
        this.name = value;
    };
    // Set's
    ProdutoComponent.prototype.getReleaseToSale = function () {
        return this.releaseToSale;
    };
    ProdutoComponent.prototype.setReleaseToSale = function (value) {
        this.releaseToSale = value;
    };
    return ProdutoComponent;
}());
exports.ProdutoComponent = ProdutoComponent;
//# sourceMappingURL=produto.component.js.map