
import { FormGroup } from '@angular/forms';


export function ValidateWithZero(controlName: string) {
    return (formGroup: FormGroup) => {
        debugger;
        const control = formGroup.controls[controlName];

        if (control.value=="0") {
            control.setErrors({ validWithZero: true });
        } else {
            control.setErrors(null);
        }
    }
}