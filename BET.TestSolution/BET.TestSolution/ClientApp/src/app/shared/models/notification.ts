import { ToastrService } from 'ngx-toastr';

export class Notification {
  toastrOptions: {
    progressBar: boolean;
    positionClass: 'toast-top-center';
  };

  constructor(private toastr: ToastrService) {}

  success(message: string, title: string = 'Success!') {
    this.toastr.success(message, title, this.toastrOptions);
  }

  error(message: string, title: string = 'Error!') {
    this.toastr.error(message, title, this.toastrOptions);
  }

  info(message: string, title: string = '') {
    this.toastr.info(message, title, this.toastrOptions);
  }

  warning(message: string, title: string = 'Warning!') {
    this.toastr.warning(message, title, this.toastrOptions);
  }

  clear() {
    this.toastr.clear();
  }
}
