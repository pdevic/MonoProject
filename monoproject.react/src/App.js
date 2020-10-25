import React, { Component } from 'react';
import axios from 'axios';

import { Modal, ModalHeader, ModalBody, ModalFooter, Table, Button, FormGroup, Label, Input } from 'reactstrap';
import ReactPaginate from 'react-paginate';

var DatePicker = require("reactstrap-date-picker");


class App extends Component {
  state = {
    externalData: null,
    newGameModal: false,
    editGameModal: false,

    newTag: '',
    tagsList: [],
    paging: null,

    newGameData: {
      ID: null,
      Name: '',
      Description: '',      
      ReleaseDate: new Date().toISOString(),
      Tags: []
    },

    editGameData: {
      ID: null,
      Name: null,
      Description: null,
      ReleaseDate: null,
      Tags: []
    }
  }

  async updateGameList() {
    await axios.get('https://localhost:44362/api/games/index').then(externalData => {
      this.setState({externalData});

      let { paging } = this.state;
      paging = externalData.data.Paging;
      this.setState({paging});
    });

  }

  componentDidMount() {
    this._asyncRequest = () => {
      this._asyncRequest = null;
    };

    this.updateGameList();
  }

  componentWillUnmount() {
    if (this._asyncRequest) {
      this._asyncRequest.cancel();
    }
  }

  submitGame(gameID) {
    if (gameID == null) {
      axios.post('https://localhost:44362/api/games/create', this.state.newGameData).then((response) => {
        this.updateGameList();
      });
    }
    else {
      let { editGameData } = this.state;
      editGameData.ID = gameID;
      editGameData.Tags = this.state.tagsList;
      this.setState({editGameData});

      this.toggleEditGameModal();

      axios.put('https://localhost:44362/api/games/edit', this.state.editGameData).then((response) => {
        this.updateGameList();
      });
    }
  }

  submitDelete(gameID) {
    axios.delete('https://localhost:44362/api/games/delete/' + gameID).then((response => {
      this.updateGameList();
    }));
  }

  updateDateValue(value, formattedValue) {
    let { newGameData } = this.state;
    newGameData.ReleaseDate = value;
    newGameData.dateFormattedValue = formattedValue;
    this.setState({newGameData});
  }

  toggleNewGameModal() {
    this.setState({ newGameModal: !this.state.newGameModal });
  }

  updateEditGame(game) {
    this.toggleEditGameModal();

    if (!this.state.editGameModal) {
      let { editGameData } = this.state;

      editGameData.ID = game.ID;
      editGameData.Name = game.Name;
      editGameData.Description = game.Description;
      editGameData.ReleaseDate = game.ReleaseDate;
      this.setState({editGameData});

      let { tagsList } = this.state;
      tagsList = game.Tags.slice();
      this.setState({tagsList});
    }
  }

  toggleEditGameModal() {
    this.setState({ editGameModal: !this.state.editGameModal });
  }

  addNewTag() {
    //console.log(this.state.newTag);
    let { tagsList } = this.state;
    tagsList.push(this.state.newTag);
    this.setState({tagsList});
  }

  removeTag(tag) {
    let { tagsList } = this.state;
    let i = tagsList.indexOf(tag);
    tagsList.splice(i, 1);
    this.setState({tagsList});
  }

  render() {
    if (this.state.externalData == null){
      return(<div className="container">Loading...</div>);
    }
    else {
      let games = this.state.externalData.data.Data.map((game) => {
        return(
          <>
          <tr key={game.ID}>
            <td>{game.Name}</td>
            <td>{game.Description}</td>
            <td>{game.ReleaseDate}</td>
            <td>
              <Button color="success" size="sm" className="mx-1" onClick={this.updateEditGame.bind(this, game)}>Edit</Button>
              <Button color="danger" size="sm" className="mx-1" onClick={this.submitDelete.bind(this, game.ID)}>Delete</Button>
            </td>
          </tr>
          <tr key={game.ID + '-tags'}>
            {game.Tags.map((tag) => (
              <Button color="info" size="sm" className="m-1">{tag}</Button>
            ))}
          </tr>
          </>
        )
      });

      return (
        <div className="App container">

          <div>
            <Button className="m-3" color="primary" onClick={this.toggleNewGameModal.bind(this)}>Add a new game</Button>
            <Modal isOpen={this.state.newGameModal} toggle={this.toggleNewGameModal.bind(this)}>
              <ModalHeader toggle={this.toggleNewGameModal.bind(this)}>Submit a new game</ModalHeader>
              <ModalBody>

                <FormGroup>
                  <Label for="gameName">Name</Label>
                  <Input type="text" id="gameName" value={this.state.newGameData.Name} onChange={(v) => {
                    let { newGameData } = this.state;
                    newGameData.Name = v.target.value;
                    this.setState({newGameData}); }} />
                </FormGroup>                
                <FormGroup>
                  <Label for="gameName">Description</Label>
                  <Input type="text" id="gameDescription" value={this.state.newGameData.Description} onChange={(v) => {
                    let { newGameData } = this.state;
                    newGameData.Description = v.target.value;
                    this.setState({newGameData}); }} />
                </FormGroup>
                <FormGroup>
                  <Label>Release date</Label>
                  <DatePicker id="gameReleaseDate" value={this.state.newGameData.ReleaseDate} onChange= {(v,f) => this.updateDateValue(v, f)} />
                </FormGroup>

              </ModalBody>
              <ModalFooter>
                <Button color="primary" onClick={this.submitGame.bind(this, null)}>Submit</Button>{' '}
                <Button color="secondary" onClick={this.toggleNewGameModal.bind(this)}>Cancel</Button>
              </ModalFooter>
            </Modal>
          </div>
          <div>
            <Modal isOpen={this.state.editGameModal} toggle={this.toggleEditGameModal.bind(this)}>
              <ModalHeader toggle={this.toggleEditGameModal.bind(this)}>Edit game</ModalHeader>
              <ModalBody>

                <FormGroup>
                  <Label for="gameName">Name</Label>
                  <Input type="text" id="gameName" value={this.state.editGameData.Name} onChange={(v) => {
                    let { editGameData } = this.state;
                    editGameData.Name = v.target.value;
                    this.setState({editGameData}); }} />
                </FormGroup>                
                <FormGroup>
                  <Label for="gameName">Description</Label>
                  <Input type="text" id="gameDescription" value={this.state.editGameData.Description} onChange={(v) => {
                    let { editGameData } = this.state;
                    editGameData.Description = v.target.value;
                    this.setState({editGameData}); }} />
                </FormGroup>
                <FormGroup>
                  <Label>Release date</Label>
                  <DatePicker id="gameReleaseDate" value={this.state.editGameData.ReleaseDate} onChange= {(v,f) => this.updateDateValue(v, f)} />
                </FormGroup>

                <Label>Tags</Label>
                <span>
                  <Input type="text" id="newTag" onChange={(v) => {
                    let { newTag } = this.state;
                    newTag = v.target.value;
                    this.setState({newTag});
                  }} />
                  <Button color="primary" onClick={this.addNewTag.bind(this)}>Add tag</Button>
                </span>
                <div>
                  {this.state.tagsList.map((tag) => (
                    <Button color="info" size="sm" className="m-1" onClick={this.removeTag.bind(this, tag)}>{tag}</Button>
                  ))}
                </div>

            </ModalBody>
            <ModalFooter>
              <Button color="primary" onClick={this.submitGame.bind(this, this.state.editGameData.ID)}>Submit</Button>{' '}
              <Button color="secondary" onClick={this.toggleEditGameModal.bind(this)}>Cancel</Button>
            </ModalFooter>
          </Modal>
          </div>

          <Table>

            <thead>
              <tr key="Categories">
                <td>Name</td>
                <td>Description</td>
                <td>Release date</td>
                <td>Actions</td>
              </tr>
            </thead>

            <tbody>
              {games}
            </tbody>

          </Table>
          
        </div>
      );
    }
  }

  

}

export default App;
