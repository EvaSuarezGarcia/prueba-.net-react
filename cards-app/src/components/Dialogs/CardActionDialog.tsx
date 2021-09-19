import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle,
} from "@mui/material";
import React from "react";
import * as Constants from "../../Constants";

interface CardActionDialogProps {
    open: boolean;
    title: string;
    body: string;
    actionButton: string;
    handleClose: () => void;
    handleAction: () => void;
}

const CardActionDialog: React.FC<CardActionDialogProps> = ({
    open,
    title,
    body,
    actionButton,
    handleClose,
    handleAction,
}) => {
    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>{title}</DialogTitle>
            <DialogContent>
                <DialogContentText>{body}</DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose}>{Constants.CANCEL}</Button>
                <Button onClick={handleAction}>{actionButton}</Button>
            </DialogActions>
        </Dialog>
    );
};

export default CardActionDialog;
