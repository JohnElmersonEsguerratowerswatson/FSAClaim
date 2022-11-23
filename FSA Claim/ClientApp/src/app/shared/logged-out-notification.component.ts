import { Component } from "@angular/core";

@Component({
  selector: 'logged-out-notif-component',
  templateUrl: 'logged-out-notification.component.html'
})
export class LoggedOutNotificationComponent {
  public notification: string;
  constructor() {
    this.notification = "This feature requires you to login";
  }
}
