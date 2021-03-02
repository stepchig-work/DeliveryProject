"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.PasswordStrengthValidator = void 0;
exports.PasswordStrengthValidator = function (control) {
    var value = control.value || '';
    if (!value) {
        return null;
    }
    var upperCaseCharacters = /[A-Z]+/g;
    if (upperCaseCharacters.test(value) === false) {
        return { passwordStrength: "Requers upper character" };
    }
    var lowerCaseCharacters = /[a-z]+/g;
    if (lowerCaseCharacters.test(value) === false) {
        return { passwordStrength: "Requers lower character" };
    }
    var numberCharacters = /[0-9]+/g;
    if (numberCharacters.test(value) === false) {
        return { passwordStrength: "Requers number" };
    }
    var specialCharacters = /[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]+/;
    if (specialCharacters.test(value) === false) {
        return { passwordStrength: "Requers special character" };
    }
    return null;
};
//# sourceMappingURL=password-strength.validator.js.map