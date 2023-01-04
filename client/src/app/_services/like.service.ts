import { Injectable, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Member } from '../_models/member';
import { MembersService } from './members.service';
import { PresenceService } from './presence.service';

@Injectable({
  providedIn: 'root',
})
export class LikeService {
  constructor(
    private memberService: MembersService,
    private toastr: ToastrService,
    public presenceService: PresenceService
  ) {}

  addLike(member: Member) {
    this.memberService.addLike(member.userName).subscribe({
      next: () => this.toastr.success('You liked ' + member.knownAs),
      error: (err: any) => console.log(err.error),
      complete: () => console.log('You liked ' + member.knownAs),
    });
  }
}
