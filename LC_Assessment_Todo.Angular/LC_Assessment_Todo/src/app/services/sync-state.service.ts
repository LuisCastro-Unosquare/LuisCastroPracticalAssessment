import { Injectable, signal } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class SyncStateService {

  isSyncing = signal<boolean>(false);

  setSyncingStateOn(state: boolean): void {
    this.isSyncing.update(x => x = true);
  }

  setSyncingStateOff(state: boolean): void {
    this.isSyncing.update(x => x = true);
  }
}
