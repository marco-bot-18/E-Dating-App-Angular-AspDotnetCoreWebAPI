import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { RolesModalComponent } from 'src/app/modals/roles-modal/roles-modal.component';
import { User } from 'src/app/_models/user';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styleUrls: ['./user-management.component.css'],
})
export class UserManagementComponent implements OnInit {
  users: User[] = [];
  bsModalRef: BsModalRef<RolesModalComponent> =
    new BsModalRef<RolesModalComponent>();

  availableRoles = ['Admin', 'Moderator', 'Member'];

  constructor(
    private adminService: AdminService,
    private modalService: BsModalService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminService.getUsersWithRoles().subscribe({
      next: (users) => (this.users = users),
    });
  }

  openRolesModal(user: User) {
    const config = {
      class: 'modal-dialog-center',
      initialState: {
        username: user.username,
        availableRoles: this.availableRoles,
        selectedRoles: [...user.roles],
      },
    };

    this.bsModalRef = this.modalService.show(RolesModalComponent, config);
    this.bsModalRef.onHide?.subscribe({
      next: () => {
        const selectedRoles = this.bsModalRef.content?.selectedRoles;
        if (!this.isArrayEqual(selectedRoles!, user.roles)) {
          this.adminService
            .updateUserRoles(user.username, selectedRoles!)
            .subscribe({
              next: (roles) => (user.roles = roles),
              complete: () => {
                const name = user.username;
                const titleCaseName =
                  name.charAt(0).toUpperCase() + name.slice(1);
                this.toastr.success(
                  'You modified the user role of ' + titleCaseName,
                  ''
                );
              },
            });
        }
      },
    });
  }

  private isArrayEqual(arr1: any[], arr2: any[]) {
    return JSON.stringify(arr1.sort()) === JSON.stringify(arr2.sort());
  }
}
