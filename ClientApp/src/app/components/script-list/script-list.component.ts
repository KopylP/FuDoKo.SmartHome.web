import { Component, OnInit, Input } from '@angular/core';
import { ScriptService } from '../../services/script.service';
import { Script } from '../../interfaces/Script';
import { Controller } from '../../interfaces/Controller';
import { ScriptEditService } from '../../services/script-edit.service';

@Component({
  selector: 'app-script-list',
  templateUrl: './script-list.component.html',
  styleUrls: ['./script-list.component.less']
})
export class ScriptListComponent implements OnInit {

    @Input() controller: Controller;

    scripts: Script[];

    constructor(private scriptService: ScriptService,
        private scriptEditService: ScriptEditService) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.scriptService.all(this.controller.id).subscribe(res => {
            this.scripts = res;
            console.log(this.scripts);
        });
    }

    onDeleteScript(script: Script) {
        const index = this.scripts.findIndex(s => s.id == script.id);
        this.scripts.splice(index, 1);
    }

    addScript() {
        this.scriptEditService.open(false, this.controller.id)
            .afterClosed().subscribe(res => {
                if (typeof res !== "undefined") {
                    this.scripts.push(res);
                }
            });
    }

}
