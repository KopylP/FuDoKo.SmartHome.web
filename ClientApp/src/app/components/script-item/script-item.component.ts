import { Component, OnInit, Input, ViewChild, Output, EventEmitter, ElementRef, SimpleChanges, OnChanges } from '@angular/core';
import { faBolt } from '@fortawesome/free-solid-svg-icons';
import { Script } from '../../interfaces/Script';
import { ScriptService } from '../../services/script.service';
import { MatMenuTrigger } from '@angular/material';

@Component({
    selector: 'app-script-item',
    templateUrl: './script-item.component.html',
    styleUrls: ['./script-item.component.less']
})
export class ScriptItemComponent implements OnInit, OnChanges {

    scriptIcon = faBolt;

    @Input() script: Script;
    @Input() class: string;
    @Output() onDeleteScript = new EventEmitter<Script>();

    @ViewChild('menuTrigger', { static: true })
    trigger: MatMenuTrigger;

    @ViewChild("scriptElement", { static: true })
    scriptElement: ElementRef;

    constructor(private scriptService: ScriptService) { }

    ngOnInit() {
        if (this.class === 'selected') {
            this.scriptElement.nativeElement.classList.add("script-circle-fill");
        } else {
            this.scriptElement.nativeElement.classList.add("script-circle-outline");
        }
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (typeof changes['class'] !== 'undefined') {
            console.log("class changes");
            if (this.class === 'selected') {
                this.scriptElement.nativeElement.classList.remove("script-circle-outline");
                this.scriptElement.nativeElement.classList.add("script-circle-fill");
            } else {
                this.scriptElement.nativeElement.classList.remove("script-circle-fill");
                this.scriptElement.nativeElement.classList.add("script-circle-outline");
            }
        }
    }

    toggleMenu() {
        if (this.class === 'selected')
          this.trigger.openMenu();
        return false;
    }

    changeScript() {
        this.scriptService.post(this.script).subscribe(res => {
        });
    }

    deleteScript() {
        this.scriptService.delete(this.script.id).subscribe(res => {
            this.onDeleteScript.emit(this.script);
        });
    }

}
