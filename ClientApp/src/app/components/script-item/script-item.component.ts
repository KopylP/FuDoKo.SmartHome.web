import { Component, OnInit, Input, ViewChild, Output, EventEmitter } from '@angular/core';
import { faBolt } from '@fortawesome/free-solid-svg-icons';
import { Script } from '../../interfaces/Script';
import { ScriptService } from '../../services/script.service';
import { MatMenuTrigger } from '@angular/material';

@Component({
    selector: 'app-script-item',
    templateUrl: './script-item.component.html',
    styleUrls: ['./script-item.component.less']
})
export class ScriptItemComponent implements OnInit {

    scriptIcon = faBolt;

    @Input() script: Script;
    @Output() onDeleteScript = new EventEmitter<Script>();

    @ViewChild('menuTrigger', { static: true })
    trigger: MatMenuTrigger;

    constructor(private scriptService: ScriptService) { }

    ngOnInit() {
        console.log(this.script);
    }

    toggleMenu() {
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
