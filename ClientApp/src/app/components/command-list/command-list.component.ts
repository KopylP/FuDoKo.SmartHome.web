import { Component, OnInit, Input, SimpleChanges, OnChanges } from '@angular/core';
import { CommandService } from '../../services/command.service';
import { Command } from '../../interfaces/Command';
import { Script } from '../../interfaces/Script';
import { CommandEditService } from '../../services/command-edit.service';

@Component({
    selector: 'app-command-list',
    templateUrl: './command-list.component.html',
    styleUrls: ['./command-list.component.less']
})
export class CommandListComponent implements OnInit, OnChanges {

    @Input() script: Script;

    commands: Command[];

    constructor(private commandService: CommandService,
        private commandEditService: CommandEditService) { }

    ngOnInit() {
        this.loadData();
    }

    loadData() {
        this.commandService.all(this.script.id).subscribe(res => {
            this.commands = res;
        }, err => {
            console.log(err);
        });
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (typeof changes['script'] !== 'undefined') {
            if (!changes['script'].firstChange) {
                this.loadData();
            }
        }
    }

    addCommand() {
        this.commandEditService.open(false, this.script)
            .afterClosed()
            .subscribe(res => {
                if (typeof res !== "undefined") {
                    this.commands.push(res);
                }
            });
    }

    deleteCommand(command: Command) {
        const index = this.commands.findIndex(p => p.id == command.id);
        if (index != -1) {
            this.commands.splice(index, 1);
        }
    }
}
