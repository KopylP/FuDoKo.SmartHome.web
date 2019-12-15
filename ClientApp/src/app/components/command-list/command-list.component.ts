import { Component, OnInit, Input, SimpleChanges, OnChanges } from '@angular/core';
import { CommandService } from '../../services/command.service';
import { Command } from '../../interfaces/Command';
import { Script } from '../../interfaces/Script';

@Component({
    selector: 'app-command-list',
    templateUrl: './command-list.component.html',
    styleUrls: ['./command-list.component.less']
})
export class CommandListComponent implements OnInit, OnChanges {

    @Input() script: Script;

    commands: Command[];

    constructor(private commandService: CommandService) { }

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

    }
}
