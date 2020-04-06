import { Observable, Observer } from 'rxjs';
import { AbstractControl, ValidatorFn, FormGroup } from '@angular/forms';
import { debug } from 'util';
declare var $;
export class CommonMethods {

    static getLocalizedString(key: string) {
        // try {
        //     if (resource.length > 0) {
        //         var result = _.findWhere(resource, { Key: key });
        //         if (result) {
        //             return result.Value;
        //         }
        //     }
        // } catch (e) {
        // }

        return key;
    }
    static stringContains(str, prefix) {
        return str.indexOf(prefix) > -1;
    }
    static toEncode(value) {
        if (window.btoa) {
            return window.btoa(value);
        } else { //for <= IE9
            return $.base64.encode(value);
        }
    };
    static arrayBufferToBase64(buffer) {
        var binary = '';
        var bytes = new Uint8Array(buffer);
        var len = bytes.byteLength;
        for (var i = 0; i < len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }
        return window.btoa(binary);
    }
    
    static validateForm(form: FormGroup, errorMessages: any = []) {
        var errorList = [];
        Object.keys(form.controls).forEach(controlName => {
            let control = form.controls[controlName];
            let errors = control.errors;
            if (errors === null || errors.count === 0) {
                return;
            }
            // Handle the 'required' case
            if (errors.required) {
                let errorMessage = errorMessages[`${controlName}.required`];
                if (!errorMessage)
                    errorMessage = `${controlName} is required`
                errorList.push(errorMessage);
            }
            if (errors.pattern) {
                let errorMessage = errorMessages[`${controlName}.pattern`];
                if (!errorMessage)
                    errorMessage = `${controlName} is invalid`
                errorList.push(errorMessage);
            }
            if (errors.min) {
                let errorMessage = errorMessages[`${controlName}.min`];
                if (!errorMessage)
                    errorMessage = `${controlName} is invalid`
                errorList.push(errorMessage);
            }
            if (errors.validWithZero) {
                let errorMessage = errorMessages[`${controlName}.required`];
                if (!errorMessage)
                    errorMessage = `${controlName} is invalid`
                errorList.push(errorMessage);
            }

            // Handle 'minlength' case
            if (errors.minlength) {
                let errorMessage = errorMessages[`${controlName}.required`];
                if (!errorMessage)
                    errorMessage = `${controlName} is invalid`
                errorList.push(errorMessage);
            }

        });
        return errorList;
    }
  
    static removeItemFromArray(item, array) {
        var index = array.indexOf(item);
        debugger;
        if (index !== -1) array.splice(index, 1);
    }
    static insertItemInArray(array, item, index) {
        array.splice(index, 0, item);
    }

}