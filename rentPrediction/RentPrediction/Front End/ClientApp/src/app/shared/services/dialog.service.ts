import { Injectable } from '@angular/core';
import { SharedModule } from '../shared.module';
import Swal, { SweetAlertIcon } from 'sweetalert2';

@Injectable({
  providedIn: SharedModule
})
export class DialogService {

  showDialog(options): Promise<any> {
    return Swal.fire(options);
  }

  showSimpleDialog(title: string, message?: string, icon?: SweetAlertIcon): Promise<any> {
    return Swal.fire({
      title: title,
      text: message,
      icon: icon,
    });
  }

  showLoadingDialog(text: string): Promise<any> {
    return this.showDialog({
      title: "Loading...",
      text: text,
      onOpen: () => {
        this.showLoading();
      }
    });
  }

  showLoading() {
    Swal.showLoading();
    const cancelButton = Swal.getCancelButton();
    if (cancelButton) {
      cancelButton.style.display = 'none';
    }
  }
}
