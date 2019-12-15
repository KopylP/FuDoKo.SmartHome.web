import { Component, OnInit, Input, ViewChild, Output, EventEmitter, ElementRef } from '@angular/core';
import { Command } from '../../interfaces/Command';
import { MatMenuTrigger } from '@angular/material';
import { faCubes } from '@fortawesome/free-solid-svg-icons';

@Component({
    selector: 'app-command-item',
    templateUrl: './command-item.component.html',
    styleUrls: ['./command-item.component.less']
})
export class CommandItemComponent implements OnInit {

    @Input() command: Command;
    @Output() onCommandDeleted: EventEmitter<Command> = new EventEmitter<Command>();

    @ViewChild('menuTrigger', { static: true })
    trigger: MatMenuTrigger;



    commandIcon = faCubes;

    constructor() { }

    ngOnInit() {
    
    }

    toggleMenu() {
        this.trigger.openMenu();
        return false;
    }

    editCommand() {

    }

    deleteCommand() {

    }

}
