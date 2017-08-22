import { ToastrOptions } from './toasr-options';

import { Injectable } from '@angular/core';

declare let toastr: any;

@Injectable()
export class ToastrService {
    public options: ToastrOptions = new ToastrOptions();

    public success(message: string, title?: string, options?: ToastrOptions) {
        toastr.success(message, title, options ? options : this.options);
    }

    public info(message: string, title?: string, options?: ToastrOptions) {
        toastr.info(message, title, options ? options : this.options);
    }

    public warning(message: string, title?: string, options?: ToastrOptions) {
        toastr.warning(message, title, options ? options : this.options);
    }

    public error(message: string, title?: string, options?: ToastrOptions) {
        toastr.error(message, title, options ? options : this.options);
    }
}
